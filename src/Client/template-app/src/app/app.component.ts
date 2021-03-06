import { Component, OnInit } from '@angular/core';
import { ValueService } from './service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  values: Array<string>;
  valueText: string;

  constructor(private valueService: ValueService) {

  }

  ngOnInit() {
    this.valueService.getValues().subscribe(x => this.values = x.json());
  }

  postValue(value) {
    console.log('click post value: ' + value);
    this.valueService.postValue(value).subscribe(x => {
      console.log('success');
      console.log(x);
    });
  }
}
