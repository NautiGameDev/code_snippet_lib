import { useParams } from 'react-router-dom';

//Welcome page that displays after successful registeration

function Welcome() {
    const { username } = useParams();

  return (
      <div className="view v-col-center glitch-in">
          <span className="lg-font">Welcome, {username}</span>
          <span className="med-font mt-16">Please check your email for an account confirmation link</span>
      </div>
  );
}

export default Welcome;