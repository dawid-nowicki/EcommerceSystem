import { Component } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-user-clothes',
  templateUrl: './user-clothes.component.html',
  styleUrls: ['./user-clothes.component.css']
})
export class UserClothesComponent{
  public client: HttpClient;
  public clothes!: Cloth[];
  private API_URL = environment.API_URL;

  constructor(http: HttpClient) {
    this.client = http;
    this.client.get<Cloth[]>(this.API_URL + 'get-my-clothes').subscribe(result => {
      this.clothes = result;
    }, error => console.error(error));
  }

}
interface Cloth {
  id :string;
  name: string;
  imagePath: string;
  price: number;
  category: string;
  isUserOwn : boolean;
}