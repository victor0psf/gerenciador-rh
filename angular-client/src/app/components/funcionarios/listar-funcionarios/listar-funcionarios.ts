import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

interface Funcionario {
  id: number;
  nome: string;
  cargo: string;
  dataAdmissao: string;
  status: boolean;
}

@Component({
  selector: 'app-listar-funcionarios',
  imports: [CommonModule, RouterModule],
  templateUrl: './listar-funcionarios.html',
  styleUrl: './listar-funcionarios.css',
})
export class ListarFuncionarios implements OnInit {
  funcionarios: Funcionario[] = [];
  carregando = false;
  erro = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.carregarFuncionarios();
  }

  carregarFuncionarios() {
    this.carregando = true;
    this.http
      .get<Funcionario[]>('http://localhost:5114/api/Funcionarios')
      .subscribe({
        next: (dados) => {
          this.funcionarios = dados;
          this.carregando = false;
        },
        error: (err) => {
          this.erro = 'Erro ao carregar funcionários.';
          this.carregando = false;
          console.error(err);
        },
      });
  }

  verDetalhes(id: number) {
    this.router.navigate(['/funcionarios/detalhes', id]);
  }

  editar(id: number) {
    this.router.navigate(['/funcionarios/editar', id]);
  }
  voltar() {
    window.location.href = '/';
  }
}
