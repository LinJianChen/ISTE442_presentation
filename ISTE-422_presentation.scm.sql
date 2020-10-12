Create database iste442_presentation;
use iste442_presentation;

create table contact(
	contactId INT NOT NULL AUTO_INCREMENT,
	lastName VARCHAR(255) NOT NULL,
	firstName VARCHAR(255),
	email varchar(255) NOT NULL,
	phone_num varchar(255) NOT NULL,
	PRIMARY KEY(contactId)
);
insert into contact (lastname, firstName, email, phone_num) values ('Chen','Lin','test@gmail.com','111-111-1111'),
('Ann','Mai','am@test@gmail','222-222-2222'),
('Quinne','You','quinee@gmail.com','333-333-3333'),
('Mic','stopini','mciStopini@gmail.com','444-444-4444');