import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  title = 'DatingApp';
  users : any;

  constructor(private http: HttpClient) {}

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
    // // }
    ngOnInit(): void {
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
      }
      
}
