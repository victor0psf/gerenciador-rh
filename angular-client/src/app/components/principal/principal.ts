import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

interface Funcionario {
  id: number;
  nome: string;
  cargo: string;
  dataAdmissao: string; // data vindo do backend como string
}

@Component({
  selector: 'app-principal',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './principal.html',
  styleUrls: ['./principal.css'],
})
export class Principal implements OnInit {
  funcionarios: Funcionario[] = [];
  carregando = false;
  erro = '';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.listarFuncionarios();
  }

  listarFuncionarios() {
    this.carregando = true;
    this.erro = '';
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

  novoFuncionario() {
    // redirecionar para página de cadastro
    window.location.href = '/funcionarios/cadastro';
  }
}
