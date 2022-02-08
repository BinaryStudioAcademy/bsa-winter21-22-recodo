import { Component, OnInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout'

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent implements OnInit {

  public sidebarOpen:boolean = true;
  constructor(
    private observer:BreakpointObserver
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(){
    this.observer.observe(['(max-width:800px)']).subscribe(
      (res)=>{
        if(res.matches){
          this.sidebarOpen=false;
        }
        else{
          this.sidebarOpen = true;
        }
      }
    );
  }
  public sidebarToogler(){
    this.sidebarOpen = !this.sidebarOpen;
  }
}
