import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

interface Ferias {
  id: number;
  dataInicio: string;
  dataTermino: string;
  funcionarioId: number;
  funcionarioNome: string;
  statusFerias: 'Pendente' | 'Andamento' | 'Concluidas'; // Novo campo
}

@Component({
  selector: 'app-listar-ferias',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './listar-ferias.html',
  styleUrl: './listar-ferias.css',
})
export class ListarFeriasComponent implements OnInit {
  feriasList: Ferias[] = [];
  carregando = false;
  erro = '';
  router: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.carregarFerias();
  }

  carregarFerias() {
    this.carregando = true;
    this.http.get<Ferias[]>('http://localhost:5114/api/Ferias').subscribe({
      next: (dados) => {
        this.feriasList = dados;
        this.carregando = false;
      },
      error: (err) => {
        this.erro = 'Erro ao carregar férias.';
        this.carregando = false;
        console.error(err);
      },
    });
  }

  excluirFerias(id: number) {
    if (!confirm('Deseja realmente excluir esta férias?')) return;

    this.http.delete(`http://localhost:5114/api/Ferias/${id}`).subscribe({
      next: () => {
        alert('Férias excluída com sucesso.');
        this.carregarFerias();
      },
      error: (err) => {
        this.erro = 'Erro ao excluir férias.';
        console.error(err);
      },
    });
  }

  irParaCadastrarFerias() {
    window.location.href = '/ferias/cadastro';
  }

  voltar() {
    window.location.href = '/';
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pendente':
        return 'badge badge-pendente';
      case 'Andamento':
        return 'badge badge-andamento';
      case 'Concluidas':
        return 'badge badge-concluidas';
      default:
        return '';
    }
  }
}
