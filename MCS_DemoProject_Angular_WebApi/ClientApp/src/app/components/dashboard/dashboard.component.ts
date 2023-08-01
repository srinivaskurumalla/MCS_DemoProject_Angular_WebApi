import { Component, OnInit } from '@angular/core';
import { DbService } from 'src/app/services/db.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

 Users: any=[];
  constructor(private _dbService : DbService) { }

  ngOnInit(): void {
    this._dbService.getAllUsers().subscribe(
      res => {
        this.Users = res
      },
      err => {
        console.log(err);
      }
    )
  }

}
