import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Funcionario } from '../../../models/funcionario.model';

interface Ferias {
  id?: number;
  dataInicio: string;
  dataTermino: string;
  funcionarioId: number;
  funcionario?: Funcionario;
}

@Component({
  selector: 'app-ferias-form',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './ferias-form.html',
  styleUrl: './ferias-form.css',
})
export class FeriasFormComponent implements OnInit {
  ferias: Ferias = {
    dataInicio: '',
    dataTermino: '',
    funcionarioId: 0,
  };

  editando = false;
  erro = '';
  carregando = false;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editando = true;
      this.carregarFerias(Number(id));
    }
  }

  carregarFerias(id: number) {
    this.carregando = true;
    this.http.get<Ferias>(`http://localhost:5114/api/Ferias/${id}`).subscribe({
      next: (dados) => {
        // Ajuste o formato da data para o input type date
        this.ferias = {
          ...dados,
          dataInicio: dados.dataInicio?.substring(0, 10) || '',
          dataTermino: dados.dataTermino?.substring(0, 10) || '',
        };
        this.carregando = false;
      },
      error: (err) => {
        this.erro = 'Erro ao carregar férias';
        this.carregando = false;
        console.error(err);
      },
    });
  }

  salvar() {
    this.carregando = true;
    if (this.editando) {
      this.http
        .put(
          `http://localhost:5114/api/Ferias/${this.ferias.id}`,
          this.ferias,
          { responseType: 'text' }
        )
        .subscribe({
          next: () => {
            this.router.navigate(['/ferias']);
            this.carregando = false;
          },
          error: (err) => {
            this.erro = 'Erro ao atualizar férias';
            this.carregando = false;
            console.error(err);
          },
        });
    } else {
      this.http
        .post('http://localhost:5114/api/Ferias', this.ferias, {
          responseType: 'text',
        })
        .subscribe({
          next: () => {
            this.router.navigate(['/ferias']);
            this.carregando = false;
          },
          error: (err) => {
            this.erro = 'Erro ao cadastrar férias';
            this.carregando = false;
            console.error(err);
          },
        });
    }
  }

  excluir() {
    if (!this.ferias.id) return;

    if (confirm('Deseja excluir as férias?')) {
      this.http
        .delete(`http://localhost:5114/api/Ferias/${this.ferias.id}`, {
          responseType: 'text',
        })
        .subscribe({
          next: () => this.router.navigate(['/ferias']),
          error: (err) => {
            this.erro = 'Erro ao excluir férias';
            console.error(err);
          },
        });
    }
  }
}
