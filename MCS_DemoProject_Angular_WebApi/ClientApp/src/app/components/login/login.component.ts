import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbService } from 'src/app/services/db.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{


  /**
   *
   */
  constructor(private _fb:FormBuilder,private _dbService :DbService,private _router : Router ) {}
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  loginForm !: FormGroup;

  formSubmitted: boolean = false;

  hideShow() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = 'fa-eye' : this.eyeIcon = 'fa-eye-slash'
    this.isText ? this.type = 'text' : this.type='password'
  }

  ngOnInit(): void {
    this.loginForm = this._fb.group({
      email: ['', Validators.required],
      password :['',Validators.required]
    })
  }

  onSubmit() {
    this.formSubmitted = true;
    debugger
    if (this.loginForm.valid) {
      //send the values to DB
      console.log(this.loginForm.value);
      this._dbService.login(this.loginForm.value).subscribe({
        next :(val: any) => {
          //var token = JSON.parse(val)
          console.log(val.token);
          console.log(val);
          this._dbService.storeToken(val.token);
          this._router.navigate(['/dashboard']);
        },
        error(err) {
          console.log(err)
          const errorMessage = err?.error?.message || 'An unknown error occurred.';
        alert(errorMessage);
          //console.log(errorMessage);
        },
      })

    } else {
      //throw error using toaster and required fields
      console.log('Form is invalid');

    }
  }

}
