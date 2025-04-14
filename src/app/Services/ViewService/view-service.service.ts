import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ViewServiceService {

  constructor() { }
  private isGridViewSubject = new BehaviorSubject<boolean>(true);
  viewState$ = this.isGridViewSubject.asObservable();

  toggleView() {
    this.isGridViewSubject.next(!this.isGridViewSubject.value);
  }
}
