# Vin Virtual Os: Design

- It is a virtual os system made like unix and linux!
- ## Dir tree:
	- ### $os_path/
	- $os_path is `./_os_env` currently
		- ``./_os_env/bin``   : will store all program binarys
		- ``./_os_env/ibin``  : interpreted binarys will be stored here
		- ``./_os_env/home`` : all user related content will be here
		- ``./_os_env/etc``   : all .json and config files will be stored here
		- ``./_os_env/trash`` : all the removed (not deleted) files will be moved here
- ## Program structure and commands:
	- this program will be made in `c#`, and this is a command line app like `linux terminal`, or just like `powershell`.
	- this program will contain these functionalities:
		- `1:` when user types `@bin  $command_name $args . . . . ` or `$command_name $args . . . . ` : then read `$os_path/bin/$command_name/BINFILE.json` for a perimeter named `latest_stable`:`$latest_stable_ene_path` then run `$os_path/bin/$command_name/$latest_stable_ene_path.exe $args . . . . `
		- `2:` when user types `@ibin $command_name $args . . . . ` : read `$os_path/ibin/$command_name/IBINFILE.json` for a perimeter named `latest_stable`:`$latest_stable_ene_path` then
			- if `$os_path/ibin/$command_name/$latest_stable_ene_path` has .py extension, like `$os_path/ibin/$command_name/$latest_stable_ene_path.py` then run `python $os_path/ibin/$command_name/$latest_stable_ene_path.py $args . . . . `
			- just like that do it for these too:
				- `.pluto` : `plutoc  $file_path`
				- `.rb`    : `ruby    $file_path`
				- `.nue`   : `nue lun $file_path`
				- `.class` (means that java .class byte code file) : `java $file_path`
		- `3:` when user types `.\$command_name $args . . . . ` : then
			- then run `.\$command_name.exe $args . . . .`
		- `4:` when user types:
			- `exit` then ask him for `do you really want to exit ($terminal_name)`, if y then exit it.
			- if command starts with: `@vm` then if command is:
				- `@vm new $vm_name` then read: `./_os_env/etc/s_opts/vm/vms.json`, if not present then create one!
					- read it to find that `$vm_name` is new name or if it already exists in that file like: `"$vm_name":"$vm_path"`,
					  then show an error!, if it is new then create a folder it in `./_os_env/home/guest/vms/$vm_name`, if command is,
					  `@vm new $vm_name --path $vm_path` then create a folder it in `./_os_env/home/$vm-path`! 
					- and after this make an entry in `./_os_env/etc/s_opts/vm/vms.json`, statinf that `"$vm_name":"$vm_path"`
					- then create these file structre in that:
					  - ### ./
						- `/workspace` : the $PINETREE shell will be opened here!
						- `/etc`       ; all the .json or config files will be stored here!
						- `/media`     : everything else will be stored in here!
						
						- then create `$vm-path/etc/shell_log.json` in which you will store data like `"$command_entered":{"time":"$at_what_time_and_date"}`
						- then create `$vm-path/workspace/README.md` in which you will will put some basic readme stuff about the @vm commands and over all commands structure!
						- then set this programs current dir to `$vm-path/` and then:
							- process some basic commands like, if commands starts with `! $commands $args ...` then run in powershell at `$vm-path/$current_path` (in which user is still present) and run `$commands $args ...` in powershell!
							- process some basic commands like, if commands starts with `!! $commands $args ...` then run in powershell at `@user_path` (in which the current host (windows) user lives `c:\users\...`) and run `$commands $args ...` in powershell!
				- `@vm load $vm_name` then find `$vm_name` from `./_os_env/etc/s_opts/vm/vms.json`, if not present then show error else, open this shell at `$vm_path from "$vm_name":"$vm_path"`
				- `@vm del  $vm_name` then find `$vm_name` from `./_os_env/etc/s_opts/vm/vms.json`, if not present then show error else, ask user 3 times that `do you really want to remove this`, if y all times then move `$vm_path` to `./_os_env/trash`, and assign this to `./etc/del_vms.json` file too
				- `@vm restore $vm_name` then find `$vm_name` dir in `./_os_env/trash` and also in `./etc/del_vms.json`, if one has and other does not then show error else , if not present then show error else, ask user 1 time that `do you really want to restre this`, if y all times then move `$vm_path` to `$vm_path from "$vm_name":"$vm_path"` which was gotten from `./_os_env/etc/s_opts/vm/vms.json`
	# YOU CAN MAKE SOME SIGNIFICIENT AND UPGRADE THE STYLE FOR MORE GOOD DEVELOPEMENT ENVOIRNMENT , PLEASE PLEASE PLEASE