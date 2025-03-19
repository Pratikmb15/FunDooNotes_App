import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { HttpService } from '../Http/http.service';

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  token: any;
  constructor(private httpService: HttpService) {
    this.token = localStorage.getItem('token')
    if (!this.token) {
      console.error(" No token found! User might not be logged in.");
    } else {
      console.log(" Token found:", this.token);
    }
   
  }

  addNotes(reqData: any) {
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`

        })

    }
    return this.httpService.PostMethodToken('http://localhost:5078/api/notes', reqData, true, header)
  }
  getNotes() {
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`
        })
    }
    return this.httpService.getService('http://localhost:5078/api/notes', true, header)
  }

  updateNotes(reqData: any, Id: any) {
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`
        })
    }
    return this.httpService.PutService(`http://localhost:5078/api/notes/${Id}`, reqData, true, header);
  }

  deleteNotes(id: any) {
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`
        })
    }
    return this.httpService.deleteServie(`http://localhost:5078/api/notes/${ id }`, true, header);
  }

  trashNotes(noteId: number) {
    let header = {
      headers: new HttpHeaders({
        'Content-type': 'application/json',
        'Authorization': `Bearer ${this.token}`
      })
    };
    return this.httpService.PutService(`http://localhost:5078/api/notes/${noteId}/ToggleTrash`, {}, true, header);
  }

  ArchiveNotes(note_Id: any) {
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`
        })
    }
    return this.httpService.PutService(`http://localhost:5078/api/notes/${note_Id}/ToggleArchive`,{}, true, header)

  }

  UpdateNotesColor(noteId:number ,reqData:any){
    let header = {
      headers: new HttpHeaders(
        {
          'Content-type': 'application/json',
          'Authorization': `Bearer ${this.token}`
        })
    }
    return this.httpService.PutService(`http://localhost:5078/api/notes/${noteId}/Color`,reqData,true,header)
  }
}