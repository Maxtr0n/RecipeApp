import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

export abstract class HttpBaseService {
    protected readonly http = inject(HttpClient);

    protected handleError(error: HttpErrorResponse): Observable<never> {
        let errorMessage = 'An error occurred';

        if (error.error instanceof ErrorEvent) {
            // Client-side error
            errorMessage = error.error.message;
        } else {
            // Server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        console.error(errorMessage);
        return throwError(() => new Error(errorMessage));
    }

    protected createParams(params: Record<string, string | number | boolean>): HttpParams {
        return Object.entries(params).reduce((httpParams, [key, value]) => {
            return httpParams.set(key, value.toString());
        }, new HttpParams());
    }
}