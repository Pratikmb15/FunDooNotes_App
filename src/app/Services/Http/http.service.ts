import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  PostMethod(reqUrl:any, reqData:any, token:boolean=false,httpOptions:any={}){
    return this.http.post(reqUrl,reqData,token&& httpOptions)
  }

  PostMethodToken(reqUrl:any, reqData:any, token:boolean=true,httpOptions:any={}){
    return this.http.post(reqUrl,reqData,token&& httpOptions)
  }
  getService(reqUrl:any,  token:boolean=true,httpOptions:any={}){
    return this.http.get(reqUrl, token&& httpOptions)
  }
  PutService(reqUrl: any, reqData: any, token: boolean = true, httpOptions: any = {}) {
    return this.http.put(reqUrl, reqData, httpOptions);  // Remove 'token&&'
  }
  
  deleteServie(reqUrl:any, token:boolean=true,httpOptions:any={}){
    return this.http.delete(reqUrl,token && httpOptions)
  }
 
}
