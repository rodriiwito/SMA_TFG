import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ObservatoriosRequest, ObservatoriosResponse } from './models';
@Injectable({
  providedIn: 'root',
})
export class ConsultasService {
  private url: string = 'http://localhost:5257/api/consulta';
  constructor(private http: HttpClient) {}

  getObservatorios(
    request: ObservatoriosRequest
  ): Observable<ObservatoriosResponse> {
    return this.http.post<ObservatoriosResponse>(
      `${this.url}/observatorios`,
      request
    );
  }
}
