import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AppUserService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.apiUrl + "User/";

  public getUserList(nameParam: string): Observable<User[]> {
    const params = new HttpParams().set("nameParam", nameParam);
    return this.http.get<User[]>(this.baseUrl + "searchList", {params: params});
  }
}
