import { SalarioFuncionario } from './salario-funcionario.model';

export interface Funcionario {
  id?: number;
  nome: string;
  cargo: string;
  dataAdmissao: string; // pode ser string ISO, você pode converter para Date no frontend se quiser
  status: boolean;
  salarios?: SalarioFuncionario[];
}
