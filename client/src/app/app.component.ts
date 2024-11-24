import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from "./nav/nav.component";
import { RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';
import { HomeComponent } from './home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet,NavComponent]
})
export class AppComponent implements OnInit {
  //http = inject(HttpClient);
  private accountService = inject(AccountService);
  title = 'DatingApp';
  ngOnInit(): void {
    // this.getUsers();
    this.setCurrentUser();
  }
  setCurrentUser()
  {
      const userString = localStorage.getItem('user');
      if(!userString) return;
      const user= JSON.parse(userString);
      this.accountService.currentUser.set(user);
  }
 /* getUsers()
  {
    this.http.get('http://localhost:5288/api/users').subscribe({
      next: response => this.users= response,
      error(err) {
        console.log("Error")
      },
      complete() {
        console.log("Request Has Completed")
      },
      
    });
  }*/
    // // ngOnInit(): void {
    //     const url = "https://localhost:5288/api/users"; 
    //     this.http.get(url).subscribe({
    //         next: (data) => {
    //             this.users = data;
    //         },
    //         error: (error) => {
    //             console.log('Error fetching API data:', error);
    //         }
    //     });
    //}
    /*ngOnInit(): void {
        const url = "http://localhost:5288/api/users"; 
        this.http.get(url).subscribe({
          next: (data) => {
            console.log('Data received:', data);
            this.users = data;
          },
          error: (error) => {
            console.log('Error fetching API data:', error);
            if (error.status === 0) {
              console.error('Network error or CORS issue');
            } else {
              console.error(`HTTP error ${error.status}: ${error.statusText}`);
            }
          }
        });
      }*/
      
}
