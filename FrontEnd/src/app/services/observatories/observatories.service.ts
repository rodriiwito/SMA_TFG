import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservatoriesRequest, ObservatoriesResponse } from '../models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ObservatoriesService {
  private url: string = 'http://localhost:5257/api/observatories';

  constructor(private http: HttpClient) {}

  getObservatories(
    request: ObservatoriesRequest
  ): Observable<ObservatoriesResponse> {
    return this.http.post<ObservatoriesResponse>(`${this.url}`, request);
  }
}
