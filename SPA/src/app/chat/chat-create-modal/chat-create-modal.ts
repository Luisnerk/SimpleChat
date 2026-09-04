import { Component, inject, Signal, signal, TemplateRef, ViewChild, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbSlide } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { Participant } from '../../_models/participant';
import { catchError, debounce, debounceTime, isEmpty, map, Observable, Subject, switchMap } from 'rxjs';
import { AppUserService } from '../../_services/app-user-service';
import { AsyncPipe } from '@angular/common';
import { User } from '../../_models/user';
import { ChatService } from '../../_services/chat-service';
import { AuthService } from '../../_services/auth-service';
import { currentUser } from '../../_models/current-user';

@Component({
  selector: 'app-chat-create-modal',
  imports: [ReactiveFormsModule, FormsModule, NgSelectModule, AsyncPipe],
  templateUrl: './chat-create-modal.html',
  styleUrl: './chat-create-modal.css',
})
export class ChatCreateModal {
  @ViewChild('content') modalContent!: TemplateRef<any>;

  private modalService = inject(NgbModal);
  private authService = inject(AuthService);
  private modalRef: NgbModalRef | undefined;
  private searchTerm = new Subject<string>();
  private currentUser: Signal<currentUser | null> = this.authService.currentUser;

  close: WritableSignal<string> = signal('');
  chatForm: FormGroup = new FormGroup({});
  users$: Observable<User[]> | undefined;
  selectedUsers: any[] = [];

  constructor(private fb: FormBuilder, private appUserService: AppUserService, private chatService: ChatService) {}
  
  ngOnInit() {
    this.initializeForm();
    if (this.currentUser()){
      this.users$ = this.searchTerm.pipe(
        debounceTime(400),
        switchMap((term: string) => 
          this.appUserService.getUserList(term).pipe(
            map((users) => {return users.filter(user => user.name != this.currentUser()?.name)}),
            catchError(() => [])
          )
        )
      );
    }
  }

  initializeForm() {
    this.chatForm = this.fb.group({
      name: ["", Validators.required],
      userIds: [[], Validators.required]
    })
  }

  registerChat() {
    this.chatService.createChat(this.chatForm.value).subscribe({
      next: res => {
        if (res.status == 200){
          this.modalRef?.close();
          this.chatForm.setValue({
            name: "",
            userIds: []
          })
        }
      },
    })
  }

  open() {
    this.modalRef = this.modalService.open(this.modalContent, { ariaLabelledBy: 'modal-basic-title' });
  }

  lookFor(value: string) {
    if (value.trim())
      this.searchTerm.next(value.toLowerCase());
  }
}
