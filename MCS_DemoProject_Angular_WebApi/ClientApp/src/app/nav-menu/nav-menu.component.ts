import { Component } from '@angular/core';
import { DbService } from '../services/db.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
constructor(private _dbService : DbService){}
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  logout() {
    this._dbService.logout();
  }

  loggedIn(): boolean {
    if (this._dbService.isLoggedIn()) {
      return true;
    }
    else return false;
  }
}
