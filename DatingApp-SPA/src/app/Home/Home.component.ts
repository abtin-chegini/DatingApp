import { Component, OnInit } from '@angular/core';

import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  values : any;
  constructor( private http: HttpClient) { }


  ngOnInit() {
    this.getvalues();
  }


  registerToggle(){

this.registerMode = !this.registerMode;


  }

  getvalues () {

     this.http.get('http://localhost:5000/api/Values').subscribe(
      response => {
    this.values = response;
    
    }, error => {
    
      console.log(error);
    });
    
    
    }


}
