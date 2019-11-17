import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";

@Injectable()
export class ApiService {

constructor(
    private http: HttpClient
) { }

    private apiServer = "https://okv2019owb.execute-api.ap-southeast-1.amazonaws.com/Prod/";
    sub: any;

     // Get
     findFace(filename: string): Observable<string> {
        const url = `${this.apiServer}api/Face/SearchFace`;
        const Params = new HttpParams().set("filename", filename);
        this.sub = this.http.get<string>(url, { params: Params });

        return this.sub;
    }
}
