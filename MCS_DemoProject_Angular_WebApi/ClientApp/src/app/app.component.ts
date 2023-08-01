import { Component } from '@angular/core';
import { DbService } from './services/db.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  constructor(private _dbService: DbService) { }
  loggedIn() {
    return this._dbService.isLoggedIn();
  }
}
