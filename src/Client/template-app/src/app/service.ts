import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http'

@Injectable()
export class ValueService {
    constructor(private http: Http) {

    }
    getValues() {
        return this.http.get('http://localhost:8273/api/values');
    }

    postValue(value) {
        console.log("sending value:")
        console.log(value);
        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        const options = new RequestOptions({ headers: headers });
        return this.http.post("http://localhost:8273/api/values", '"' + value + '"', options);
    }
}