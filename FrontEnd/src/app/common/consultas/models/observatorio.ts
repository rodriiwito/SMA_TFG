import { BaseResponse } from './base-response';

export interface ObservatoriosRequest {
  alturaMinima: number | null;
  alturaMaxima: number | null;
  nombre: string | null;
}

export interface ObservatoriosResponse extends BaseResponse {
  observatorios: Observatorio[];
}

export interface Observatorio {
  numero: string;
  nombre: string;
  longitud: string;
  latitud: number;
  altura: number;
  creditos: number;
}
