import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

interface SalarioFuncionarioDTO {
  salario: number;
  salarioMedio: number;
}

interface Funcionario {
  id?: number;
  nome: string;
  cargo: string;
  dataAdmissao: string;
  status: boolean;
  salarios: SalarioFuncionarioDTO[];
}

@Component({
  selector: 'app-funcionario-form',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './funcionario-form.html',
  styleUrl: './funcionario-form.css',
})
export class FuncionarioForm implements OnInit {
  funcionario: Funcionario = {
    nome: '',
    cargo: '',
    dataAdmissao: '',
    status: true,
    salarios: [{ salario: 0, salarioMedio: 0 }],
  };

  funcionarioOriginal: Funcionario | null = null;
  carregando = false;
  erro = '';
  editando = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editando = true;
      this.carregarFuncionario(Number(id));
    }
  }

  carregarFuncionario(id: number) {
    this.carregando = true;
    this.http
      .get<Funcionario>(`http://localhost:5114/api/Funcionarios/${id}`)
      .subscribe({
        next: (dados) => {
          this.funcionario = {
            ...dados,
            dataAdmissao: dados.dataAdmissao?.substring(0, 10) || '',
            salarios: dados.salarios?.length
              ? dados.salarios
              : [{ salario: 0, salarioMedio: 0 }],
          };
          this.funcionarioOriginal = JSON.parse(
            JSON.stringify(this.funcionario)
          );
          this.carregando = false;
        },
        error: (err) => {
          this.erro = 'Erro ao carregar funcionário.';
          this.carregando = false;
          console.error(err);
        },
      });
  }

  salvar() {
    this.carregando = true;
    this.erro = '';

    // Validação de salários
    if (!this.funcionario.salarios?.length) {
      this.funcionario.salarios = [{ salario: 0, salarioMedio: 0 }];
    }

    if (this.editando && this.funcionarioOriginal) {
      const funcionarioParaEnviar: any = {};

      if (this.funcionario.nome !== this.funcionarioOriginal.nome) {
        funcionarioParaEnviar.nome = this.funcionario.nome;
      }

      if (this.funcionario.cargo !== this.funcionarioOriginal.cargo) {
        funcionarioParaEnviar.cargo = this.funcionario.cargo;
      }

      if (
        this.funcionario.dataAdmissao !== this.funcionarioOriginal.dataAdmissao
      ) {
        funcionarioParaEnviar.dataAdmissao = new Date(
          this.funcionario.dataAdmissao
        ).toISOString();
      }

      if (this.funcionario.status !== this.funcionarioOriginal.status) {
        funcionarioParaEnviar.status = this.funcionario.status;
      }

      const salarioAtual = this.funcionarioOriginal.salarios?.[0] || {
        salario: 0,
        salarioMedio: 0,
      };
      const salarioNovo = this.funcionario.salarios?.[0] || {
        salario: 0,
        salarioMedio: 0,
      };

      if (
        salarioNovo.salario > 0 &&
        salarioNovo.salario !== salarioAtual?.salario
      ) {
        funcionarioParaEnviar.salarios = funcionarioParaEnviar.salarios || [{}];
        funcionarioParaEnviar.salarios[0].salario = salarioNovo.salario;
      }

      if (
        salarioNovo.salarioMedio > 0 &&
        salarioNovo.salarioMedio !== salarioAtual?.salarioMedio
      ) {
        funcionarioParaEnviar.salarios = funcionarioParaEnviar.salarios || [{}];
        funcionarioParaEnviar.salarios[0].salarioMedio =
          salarioNovo.salarioMedio;
      }

      this.http
        .put(
          `http://localhost:5114/api/Funcionarios/${this.funcionario.id}`,
          funcionarioParaEnviar
        )
        .subscribe({
          next: () => {
            this.carregando = false;
            this.router.navigate(['/funcionarios']);
          },
          error: (err) => {
            this.erro = 'Erro ao salvar funcionário.';
            this.carregando = false;
            console.error(err.error);
          },
        });
    } else {
      this.http
        .post('http://localhost:5114/api/Funcionarios', this.funcionario)
        .subscribe({
          next: () => {
            this.carregando = false;
            this.router.navigate(['/funcionarios']);
          },
          error: (err) => {
            this.erro = 'Erro ao salvar funcionário.';
            this.carregando = false;
            console.error(err.error);
          },
        });
    }
  }
  voltar() {
    window.location.href = '/';
  }
}
