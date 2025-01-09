# README.MD

## Overview 
This project consists of a .NET application which is hosted on an AWS EC2 instance connected to an RDS database within the same VPC. The front-end is a React.js application hosted on Amazon S3. A CI/CD pipeline automates deployment of the applications when code is pushed to GitHub.

![arc-1-overview-small](https://github.com/user-attachments/assets/8960983a-cef5-40fd-b6ad-444399d35ce3)


## Architecture Diagram
![arc-1-detail1](https://github.com/user-attachments/assets/4a1351a4-1c07-40ce-adcd-0f56016d0564)

### 1. VPC and Subnets
- The VPC is divided into four subnets: two public subnets and two private subnets.
- Each subnet has its own route table.
- The private subnet route tables connect to the internet through an Internet Gateway (IGW).

<img src="https://github.com/user-attachments/assets/06182dcb-f743-4be5-8ce9-dcc4aa4e8cd9" style="width:40vw" />

### 2. Bastion Host
- A bastion host is deployed in one of the public subnets to provide secure SSH access to resources within the private subnets.

### 3. .NET Application on EC2
- The .NET application is deployed on an AWS EC2 instance.
- The EC2 instance resides in a public subnet of the VPC.

### 4. Database Setup
- The RDS instance uses MySQL as the database engine.
- The RDS instance is hosted in two private subnets within the same VPC for high availability.

<img src="https://github.com/user-attachments/assets/86fa0580-aa4a-4375-9e3d-a19ba538f170" style="width:40vw" />

### 5. React.js Frontend
- The React.js application is hosted on Amazon S3 with static website hosting enabled.
- It connects to the .NET application API over the internet.

<img src="https://github.com/user-attachments/assets/67362673-eedc-4797-98cb-0428520b7809" style="width:40vw" />
<img src="https://github.com/user-attachments/assets/543302ff-cda6-4c64-9179-a62cac58ad62" style="width:40vw" />
<img src="https://github.com/user-attachments/assets/f884e74d-c347-41b1-a631-cb1f85eece72" style="width:40vw" />

# CI/CD Pipeline
![cicd](https://github.com/user-attachments/assets/fc917e65-a5ba-4cc5-8668-d02f9b6bc100)

### 1. GitHub Actions
- Triggers when code is pushed to the repository.

### 2. Backend Deployment
- Checks out the code.
- Builds and publishes the .NET application.
- Creates a Docker image for the application.
- Pushes the Docker image to Docker Hub.
- Deploys the updated application to the EC2 instance.

### 3. Frontend Deployment
- Builds the React.js application.
- Deploys the static files to the S3 bucket.

## Future Enhancements
- Implement load balancing for the .NET backend using an AWS Application Load Balancer.
- Add monitoring and logging using AWS CloudWatch.

## Useful Links
- https://www.piliapp.com/symbol/quotation-mark/
- https://app.haikei.app/
- https://colorhunt.co/
- https://fonts.google.com/