import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-weather-inspiration',
  templateUrl: './weather-inspiration.component.html',
  styleUrls: ['./weather-inspiration.component.css']
})
export class WeatherInspirationComponent {
  public keyword = 'city';
  private client: HttpClient;
  private API_URL = environment.API_URL;

  public cities!: City[]
  public inspirations!: Cloth[][];
  public inspiration!: Cloth[];
  private index!: number;

  constructor(http: HttpClient) {
    this.client = http;
  }


  getInspiration(city: City) {
    var localization = city.city;
    this.client.get<Cloth[][]>(this.API_URL + 'get-outfits-by-localization?localization=' + localization).subscribe(result => {

      this.inspirations = result;
      this.inspiration = this.inspirations[0];
      this.index = 0;

    }, error => console.error(error));
  }

  buyCloth(cloth: Cloth) {
    this.client.get<boolean>(this.API_URL + 'buy?clothId=' + cloth.id
    ).subscribe(result => {
      cloth.isUserOwn = result;
    }, error => console.error(error));
  }

  hasNext() {
    if (this.inspirations && this.inspirations.length == 2) {
      return true;
    }
    return false;
  }

  goNext() {
    this.index = this.index == 0 ? 1 : 0;
    this.inspiration = this.inspirations[this.index];
    window.scroll(0, 0);
  }

  getCities(val: string) {
    this.client.get<City[]>(this.API_URL + 'get-cities?word=' + val).subscribe(result => {
      this.cities = result;
    }, error => console.error(error));
  }

  onChangeSearch(val: string) {
    this.getCities(val);
  }
}
interface Cloth {
  id: number;
  name: string;
  imagePath: string;
  price: number;
  category: string;
  isUserOwn: boolean;
}
interface City {
  city: string;
}