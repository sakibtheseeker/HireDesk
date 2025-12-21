create database HireDesk;


-- TABLE APPLICANT 
create table Applicant(
aid int primary key identity,
educationStream varchar(30) not null,
experienceType varchar(20) not null,
aName varchar(50) not null,
aEmail varchar(50) not null unique,
aContact VARCHAR(10) NOT NULL unique,
aCTC decimal(12,2),
aECTC decimal(12,2),
aNoticePeriod varchar(30),
appliedDate DATETIME NOT NULL DEFAULT SYSDATETIME(),
aStatus VARCHAR(20) NOT NULL DEFAULT 'Applied'
);


-- TABLE InterviewSlot

create table InterviewSlot(
sid int primary key identity,
sDay varchar(30) not null,
sTime varchar(30) not null)
;

-- TABLE ApplicantInterview

create table ApplicantInterview(
aiid int primary key identity,
aid int not null ,
sid int not null,
bookedDate datetime not null default sysdatetime(),
constraint FK_AI_Applicant
foreign key (aid) references Applicant(aid),

constraint FK_AI_Slot
foreign key (sid) references InterviewSlot(sid),

constraint UQ_AI_Applicant unique (aid)
);


-- Stored Procedures

-- SP FOR APPLICANT TABLE
create proc FetchAllApplicants
as 
begin
 select * from Applicant
end

exec FetchAllApplicants;

-- EXPERIENCED APPLICANT
create proc AddExperiencedApplicant
@educationStream varchar(30),
@experienceType varchar(20),
@aName varchar(50),
@aEmail varchar(50),
@aContact VARCHAR(10),
@aCTC decimal(12,2),
@aECTC decimal(12,2),
@aNoticePeriod varchar(30)
as
begin
 insert into Applicant(
  educationStream,experienceType,
aName,aEmail,aContact ,
aCTC,aECTC,aNoticePeriod)
 values
 (
 @educationStream ,@experienceType,
@aName ,@aEmail,@aContact ,
@aCTC,@aECTC ,@aNoticePeriod );
end

-- FRESHER APPLICANT
create proc AddFresherApplicant
@educationStream varchar(30),
@experienceType varchar(20),
@aName varchar(50),
@aEmail varchar(50),
@aContact VARCHAR(10)
as
begin
 insert into Applicant(
  educationStream,experienceType,
aName,aEmail,aContact )
 values
 (
 @educationStream ,@experienceType,
@aName ,@aEmail,@aContact );
select scope_identity() as aid;
end


-- SP FOR InterviewSlot Table
create proc FetchAllInterviewSlot
as 
begin
 select * from InterviewSlot
end

exec FetchAllInterviewSlot;


create proc AddInterviewSlot
@sDay varchar(30),
@sTime varchar(30)
as
begin
 insert into InterviewSlot(
  sDay ,sTime)
 values
 (
 @sDay ,@sTime)
end


-- SP FOR ApplicantInterview Table
create proc FetchAllApplicantInterview
as 
begin
 select * from ApplicantInterview
end

exec FetchAllApplicantInterview;

create proc AddApplicantInterview
@aid int,
@sid int
as
begin
 insert into ApplicantInterview(
 aid,sid )
 values
 (
 @aid,@sid)
end

