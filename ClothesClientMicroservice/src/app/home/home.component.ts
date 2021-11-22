import { Component } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public client: HttpClient;
  public categories!: Category[];
  public clothes!: Cloth[];
  private API_URL = environment.API_URL;

  constructor(http: HttpClient) {
    this.client = http;
    this.client.get<Category[]>(this.API_URL + 'get-categories').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
    this.getAllClothes();
  }
  getAllClothes() {
    return this.client.get<Cloth[]>(this.API_URL + 'get-all-clothes').subscribe(result => {
      this.clothes = result;
    }, error => console.error(error));
  }
  getClothesByCategory(categoryId: number) {
    return this.client.get<Cloth[]>(this.API_URL + 'get-clothes-by-category?categoryId=' + categoryId).subscribe(result => {
      this.clothes = result;
    }, error => console.error(error));
  }
  buyCloth(cloth: Cloth) {
    this.client.get<boolean>(this.API_URL + 'buy?clothId=' + cloth.id
    ).subscribe(result => {
      cloth.isUserOwn = result;
    }, error => console.error(error));
  }
}


interface Category {
  id: number;
  categoryValue: string;
}

interface Cloth {
  id: number;
  name: string;
  imagePath: string;
  price: number;
  isUserOwn: boolean;
}