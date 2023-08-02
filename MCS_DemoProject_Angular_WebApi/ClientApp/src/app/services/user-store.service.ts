import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {

  private fullName$ = new BehaviorSubject<string>("");
  private role$ = new BehaviorSubject<string>("");
  constructor() {}


  public getFullNameFromStore() {
    return this.fullName$.asObservable();
  }

  public setFullNameToStore(fullName : string) {
    this.fullName$.next(fullName);
  }

  public getRoleFromStore() {
    return this.role$.asObservable();
  }

  public setRoleToStore(role : string) {
    this.role$.next(role);
  }
}
