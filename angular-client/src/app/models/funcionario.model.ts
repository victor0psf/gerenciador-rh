import { SalarioFuncionario } from './salario-funcionario.model';

export interface Funcionario {
  id?: number;
  nome: string;
  cargo: string;
  dataAdmissao: string;
  status: boolean;
  salarios?: SalarioFuncionario[];
}
