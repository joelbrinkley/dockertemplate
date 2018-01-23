import { Component, OnInit } from '@angular/core';
import { ValueService } from './service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  values: Array<string>;

  constructor(private valueService: ValueService) {

  }

  ngOnInit() {
    this.valueService.getValues().subscribe(x => this.values = x.json());
  }
}
