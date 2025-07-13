import { Funcionario } from './funcionario.model';

export interface Ferias {
  id: number;
  dataInicio: string; // string ISO
  dataTermino: string; // string ISO
  funcionarioId: number;
  funcionario?: Funcionario;
}
