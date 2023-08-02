import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouteReuseStrategy } from '@angular/router';
import { DbService } from 'src/app/services/db.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent  implements OnInit{



  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  signUpForm !: FormGroup;
  formSubmitted: boolean = false;
  hideShow() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = 'fa-eye' : this.eyeIcon = 'fa-eye-slash'
    this.isText ? this.type = 'text' : this.type='password'
  }

  /**
   *
   */
  constructor(private _fb:FormBuilder, private _dbService : DbService,private _router : Router) {


  }

  ngOnInit(): void {

    this.signUpForm = this._fb.group({
      firstName : ['',Validators.required],
      lastName : [''],
     // userName : ['',Validators.required],
      email : ['',Validators.required],
      password : ['',Validators.required],
    })
  }

  onSubmit() {
    this.formSubmitted = true;
    if (this.signUpForm.valid) {



      console.log(this.signUpForm.value)
      this._dbService.register(this.signUpForm.value).subscribe({
        next: (val: any) => {
          alert('Registered successfully');
          this._router.navigate(['/login'])
        }, error: (err: any) => {
          alert('registration failed');
        }
      }

      )
    }
  }
}
