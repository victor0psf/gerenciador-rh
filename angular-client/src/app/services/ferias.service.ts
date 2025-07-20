import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ferias } from '../models/ferias.model';

@Injectable({
  providedIn: 'root',
})
export class FeriasService {
  private apiUrl = 'http://localhost:5114/api/Ferias';

  constructor(private http: HttpClient) {}

  getFerias(): Observable<Ferias[]> {
    return this.http.get<Ferias[]>(this.apiUrl);
  }

  getFeriasById(id: number): Observable<Ferias> {
    return this.http.get<Ferias>(`${this.apiUrl}/${id}`);
  }

  addFerias(ferias: Ferias): Observable<any> {
    return this.http.post(this.apiUrl, ferias);
  }

  updateFerias(id: number, ferias: Ferias): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, ferias);
  }

  deleteFerias(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
