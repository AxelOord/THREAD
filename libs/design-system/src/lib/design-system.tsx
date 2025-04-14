import './global.css';

export function DesignSystem() {
  return (
    <div className="flex items-center justify-center min-h-screen bg-zinc-50">
      <div className="bg-white rounded-3xl shadow-md p-10 max-w-md w-full border border-zinc-100">
        <h1>Welcome to DesignSystem!</h1>
        <p className="text-zinc-500 mt-3 leading-relaxed">Explore reusable components and styles here.</p>
        <div className="mt-8 pt-6 border-t border-zinc-100">
          <button className="px-5 py-2.5 bg-indigo-600 text-white font-medium rounded-xl hover:bg-indigo-700 transition-colors">
            Get Started
          </button>
        </div>
      </div>
    </div>
  );
}

export default DesignSystem;