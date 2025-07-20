import { Funcionario } from './funcionario.model';

export interface Ferias {
  id: number;
  dataInicio: string;
  dataTermino: string;
  funcionarioId: number;
  funcionario?: Funcionario;
}
