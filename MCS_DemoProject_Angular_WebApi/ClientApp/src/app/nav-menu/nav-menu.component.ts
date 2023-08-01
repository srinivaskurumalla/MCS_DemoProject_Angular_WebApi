import { Component, OnInit } from '@angular/core';
import { DbService } from '../services/db.service';
import { UserStoreService } from '../services/user-store.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  public fullName: string = '';
constructor(private _dbService : DbService,private _userStoreService : UserStoreService){}
  ngOnInit(): void {
    this._userStoreService.getFullNameFromStore().subscribe(
      val => {
        console.log(val)
        debugger
        let fullNameFromToken = this._dbService.getFullNameFromToken();
        this.fullName = val || fullNameFromToken;
      }
    )
   }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this.fullName = ''
    this._dbService.logout();
  }

  loggedIn(): boolean {
    if (this._dbService.isLoggedIn()) {
      return true;
    }
    else return false;
  }

}
