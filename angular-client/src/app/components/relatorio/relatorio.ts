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
  selector: 'app-relatorio',
  imports: [CommonModule, RouterModule],
  templateUrl: './relatorio.html',
  styleUrl: './relatorio.css',
})
export class Relatorio implements OnInit {
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

  baixarRelatorioPDF(): void {
    this.carregando = true;
    this.erro = '';
    this.http
      .get('http://localhost:5114/api/funcionarios/relatorio-pdf', {
        responseType: 'blob',
      })
      .subscribe({
        next: (blob) => {
          this.carregando = false;
          const url = window.URL.createObjectURL(blob);
          const link = document.createElement('a');
          link.href = url;
          link.download = 'RelatorioFuncionarios.pdf';
          link.click();
          window.URL.revokeObjectURL(url);
        },
        error: async (err) => {
          this.carregando = false;
          // Tentar extrair mensagem de erro do blob de resposta
          if (err.error instanceof Blob) {
            const textoErro = await err.error.text();
            this.erro = `Erro ao baixar relatório: ${textoErro}`;
            console.error('Erro no backend:', textoErro);
          } else {
            this.erro = 'Erro desconhecido ao baixar relatório.';
            console.error(err);
          }
        },
      });
  }
  voltar() {
    window.history.back();
  }
}
