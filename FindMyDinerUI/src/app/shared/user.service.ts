import { Injectable } from '@angular/core';
import { FormBuilder, Validators,FormGroup} from '@angular/forms';
import{HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder,private http:HttpClient) {}
  readonly BaseURI= 'http://localhost:53498/api/';
    formModel = this.fb.group({
      UserName: ['', Validators.required],
      Email: ['', [Validators.email,Validators.required]],
      PhoneNumber:[''],
      FullName: [''],   
      Passwords: this.fb.group({
        Password: ['', [Validators.required, Validators.minLength(6)]],
        ConfirmPassword: ['', Validators.required]
      },{validator:this.ComparePassword})
  
    });
    ComparePassword(fb:FormGroup){
       let confirmPswrdCtrl = fb.get('ConfirmPassword');
       //password mismatch
       if(confirmPswrdCtrl.errors==null||'passwordMismatch' in confirmPswrdCtrl.errors ){
           if(fb.get('Password').value != confirmPswrdCtrl.value)
            confirmPswrdCtrl.setErrors({passwordMismatch:true});
           }
           else{
            confirmPswrdCtrl.setErrors(null);
           }
       }

       register(){ 

        var body={
          UserName:this.formModel.value.UserName,
          Email:this.formModel.value.Email,
          FullName:this.formModel.value.FullName,
          //PhoneNumber:this.formModel.value.PhoneNumber,
          Password:this.formModel.value.Passwords.Password,
        };
        return this.http.post(this.BaseURI+'user/Register',body);
       }
       
       login(formData){
        return this.http.post(this.BaseURI+'user/Login',formData);
       }
    }

