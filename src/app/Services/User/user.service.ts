import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from '../Http/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }

  login(reqData: any) {

    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json'
        })
    }
    return this.httpService.postMethod('http://localhost:5078/api/users/login', reqData, false, header)

  }

  Register(reqData: any) {

    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json'
        })
    }
    return this.httpService.postMethod('http://localhost:5078/api/users', reqData, false, header)
  }

  forgotPassword(reqData:any){
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json'
        })
    }
    return this.httpService.postMethod('http://localhost:5078/api/users/forgot-password',reqData,false,header)
  }

  ressetPassword(reqData:any,token:any){
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json'
        })
    }
    return this.httpService.postMethod(`http://localhost:5078/api/users/set-newpassword/${token}`,reqData,false,header)
  }
}
