import { BaseResponse } from './base-response';

export interface ObservatoriesRequest {
  alturaMinima: number | null;
  alturaMaxima: number | null;
  nombre: string | null;
}

export interface ObservatoriesResponse extends BaseResponse {
  observatories: Observatory[];
}

export interface Observatory {
  numero: string;
  nombre: string;
  longitud: string;
  latitud: number;
  altura: number;
  creditos: number;
}
