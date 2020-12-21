package crc64d0dceaa787c9e0fe;


public class MyViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SampleLogin.Droid.MyViewHolder, SampleLogin.Droid", MyViewHolder.class, __md_methods);
	}


	public MyViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == MyViewHolder.class)
			mono.android.TypeManager.Activate ("SampleLogin.Droid.MyViewHolder, SampleLogin.Droid", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
