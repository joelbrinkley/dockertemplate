import { Injectable } from '@angular/core';
import { Http } from '@angular/http'

@Injectable()
export class ValueService {
    constructor(private http: Http) {

    }
    getValues() {
        return this.http.get('http://localhost:8273/api/values');
    }
}