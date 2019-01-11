import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private logInfo = true;
  private logWarn = true;
  private logError = true;

  constructor() { }

  info(message: string, optionalParams?: any[]): void {
    if (this.logInfo) {
      // tslint:disable-next-line:no-console
      console.info(message, optionalParams);
    }
  }

  error(message: string, optionalParams?: any[]): void {
    if (this.logError) {
      // tslint:disable-next-line:no-console
      console.error(message, optionalParams);
    }
  }

  exception(exception: ExceptionInformation, optionalParams?: any[]): void {
    if (this.logError) {
      // tslint:disable-next-line:no-console
      console.error(exception, optionalParams);
    }
  }

  warn(message: string, optionalParams?: any[]): void {
    if (this.logWarn) {
      // tslint:disable-next-line:no-console
      console.warn(message, optionalParams);
    }
  }
}
