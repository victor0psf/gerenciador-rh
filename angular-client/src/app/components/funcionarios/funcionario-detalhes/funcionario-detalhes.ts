import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

interface SalarioFuncionario {
  id: number;
  salario: number;
  salarioMedio: number;
  funcionarioId: number;
}

interface Funcionario {
  id: number;
  nome: string;
  cargo: string;
  dataAdmissao: string;
  status: boolean;
  salarios: SalarioFuncionario[];
}

@Component({
  selector: 'app-funcionario-detalhes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './funcionario-detalhes.html',
  styleUrl: './funcionario-detalhes.css',
})
export class FuncionarioDetalhes implements OnInit {
  funcionario?: Funcionario;
  carregando = false;
  erro = '';

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  ngOnInit() {
    this.carregarFuncionario();
  }

  carregarFuncionario() {
    this.carregando = true;
    this.erro = '';
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      this.erro = 'ID do funcionário não informado.';
      this.carregando = false;
      return;
    }

    this.http
      .get<Funcionario>(`http://localhost:5114/api/Funcionarios/${id}`)
      .subscribe({
        next: (dados) => {
          this.funcionario = dados;
          this.carregando = false;
        },
        error: (err) => {
          this.erro = 'Erro ao carregar funcionário.';
          this.carregando = false;
          console.error(err);
        },
      });
  }

  formatarDataAdmissao(): string {
    if (!this.funcionario) return 'Não informado';
    const data = new Date(this.funcionario.dataAdmissao);
    return data.toLocaleDateString('pt-BR', { year: 'numeric', month: 'long' });
  }

  formatarSalario(): string {
    if (!this.funcionario?.salarios || this.funcionario.salarios.length === 0)
      return 'Não informado';

    // Pega o salário mais recente (se quiser, pode ajustar conforme a data)
    const salarioRecente = this.funcionario.salarios.reduce((prev, curr) =>
      curr.id > prev.id ? curr : prev
    );

    if (typeof salarioRecente.salario === 'number') {
      return salarioRecente.salario.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      });
    } else {
      console.warn('Salário inválido:', salarioRecente.salario);
      return 'Salário inválido';
    }
  }

  voltar() {
    window.history.back();
  }
}
