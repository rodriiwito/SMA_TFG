import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  MeteorsRequest,
  MeteorsResponse,
  TypeDistributionResponse,
} from '../models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MeteorsService {
  private url: string = 'http://localhost:5257/api/meteors';

  constructor(private http: HttpClient) {}
  getMeteors(request: MeteorsRequest): Observable<MeteorsResponse> {
    return this.http.post<MeteorsResponse>(`${this.url}`, request);
  }

  getTypeDistribution(): Observable<TypeDistributionResponse> {
    return this.http.get<TypeDistributionResponse>(`${this.url}/distribution`);
  }
}
