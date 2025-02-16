import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  private notificationSubject = new Subject<{ message: string, type: 'success' | 'error' | 'alert' }>();

  notification$ = this.notificationSubject.asObservable();

  show(message: string, type: 'success' | 'error' | 'alert'): void {
    this.notificationSubject.next({ message, type });
  }
}
