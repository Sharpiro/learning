import { browser, element, by } from 'protractor';

export class AngularCLI.NoProjectPage {
  navigateTo() {
    return browser.get('/');
  }

  getParagraphText() {
    return element(by.css('cliNp-root h1')).getText();
  }
}
