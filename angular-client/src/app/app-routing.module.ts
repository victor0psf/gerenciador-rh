import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Importe aqui os seus componentes sem o sufixo "Component"
import { FeriasForm } from './components/ferias/ferias-form/ferias-form';
import { ListarFerias } from './components/ferias/listar-ferias/listar-ferias';
import { FuncionarioDetalhes } from './components/funcionarios/funcionario-detalhes/funcionario-detalhes';
import { FuncionarioForm } from './components/funcionarios/funcionario-form/funcionario-form';
import { ListarFuncionarios } from './components/funcionarios/listar-funcionarios/listar-funcionarios';
import { Principal } from './components/principal/principal';
import { Relatorio } from './components/relatorio/relatorio';

const routes: Routes = [
  { path: '', component: Principal },

  // Rotas Funcionários
  { path: 'funcionarios', component: ListarFuncionarios },
  { path: 'funcionarios/cadastro', component: FuncionarioForm },
  { path: 'funcionarios/editar/:id', component: FuncionarioForm },
  { path: 'funcionarios/detalhes/:id', component: FuncionarioDetalhes },

  // Rotas Férias
  { path: 'ferias', component: ListarFerias },
  { path: 'ferias/cadastro', component: FeriasForm },
  { path: 'ferias/editar/:id', component: FeriasForm },

  // Rota Relatório
  { path: 'relatorio', component: Relatorio },

  // Rota fallback para página não encontrada (opcional)
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
