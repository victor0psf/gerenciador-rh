import { Routes } from '@angular/router';
import { FeriasFormComponent } from './components/ferias/ferias-form/ferias-form';
import { ListarFeriasComponent } from './components/ferias/listar-ferias/listar-ferias';
import { FuncionarioDetalhes } from './components/funcionarios/funcionario-detalhes/funcionario-detalhes';
import { FuncionarioForm } from './components/funcionarios/funcionario-form/funcionario-form';
import { ListarFuncionarios } from './components/funcionarios/listar-funcionarios/listar-funcionarios';
import { Principal } from './components/principal/principal';
import { Relatorio } from './components/relatorio/relatorio';

export const routes: Routes = [
  { path: '', component: Principal },
  { path: 'funcionarios', component: ListarFuncionarios },
  { path: 'funcionarios/cadastro', component: FuncionarioForm },
  { path: 'funcionarios/editar/:id', component: FuncionarioForm },
  { path: 'funcionarios/detalhes/:id', component: FuncionarioDetalhes },
  { path: 'ferias', component: ListarFeriasComponent },
  { path: 'ferias/cadastro', component: FeriasFormComponent },
  { path: 'ferias/editar/:id', component: FeriasFormComponent },
  { path: 'relatorio', component: Relatorio },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
