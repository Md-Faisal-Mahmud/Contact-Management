import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactService } from '../../Services/contact.service';

@Component({
  selector: 'app-contact-create',
  templateUrl: './contactadd.component.html',
  styleUrls: ['./contactadd.component.css']
})
export class ContactaddComponent implements OnInit {
  contactForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private contactService: ContactService) { }

  ngOnInit(): void {
    this.contactForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$/)]],
      address: ['', Validators.required]
    });
  }

  // Convenience getter for easy access to form fields
  get f() { return this.contactForm.controls; }

  onSubmit() {
    // Stop here if form is invalid
    if (this.contactForm.invalid) {
      return;
    }

    // Process the form data
    const formData = this.contactForm.value;
    // Call your service method to add the contact
    // Assuming you have a method in your service to add a contact
    this.contactService.addContact(formData).subscribe(
      response => {
        // Handle success
        console.log('Contact added successfully:', response);
        // Optionally, you can reset the form here
        this.contactForm.reset();
      },
      error => {
        // Handle error
        console.error('Error adding contact:', error);
      }
    );
  }
}
