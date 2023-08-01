import { Component, OnInit } from '@angular/core';
import { DbService } from 'src/app/services/db.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public fullName: string = '';

 Users: any=[];
  constructor(private _dbService : DbService,private _userStoreService : UserStoreService) { }

  ngOnInit(): void {
    this._userStoreService.getFullNameFromStore().subscribe(
      val => {
        console.log(val)
        debugger
        let fullNameFromToken = this._dbService.getFullNameFromToken();
        this.fullName = val || fullNameFromToken;
      }
    )
    this._dbService.getAllUsers().subscribe(
      res => {
        this.Users = res
      },
      err => {
        console.log(err);
      }
    )
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
