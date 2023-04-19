import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Tienda';
  productos: any[]=[];

  constructor(private http: HttpClient){}
  
  ngOnInit(){
    this.http.get('https://localhost:5000/api/productos?pageSize=50').subscribe((Response:any) =>{
      this.productos = Response.data
    },error=>{
      console.log(error);
    }
    );
  }
}
