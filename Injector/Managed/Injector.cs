using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Lunity.Injector {
	public class Injector {

		enum ArgState {
			None,
			Inject,
			Uninject,
			Name
		}

		enum Task {
			None,
			Inject,
			Uninject
		}

		public static Process game;

		public static void Main(string[] args) {

			//Important values for what the program should do
			ArgState currentState = ArgState.None;
			Task task = Task.None;
			string moduleName = "";
			string processName = "";

			//Parse the arguments
			foreach(string arg in args) {
				if(currentState == ArgState.None) {
					if(arg == "-i") {
						currentState = ArgState.Inject;
					}
					if(arg == "-u") {
						currentState = ArgState.Uninject;
					}
					if(arg == "-n") {
						currentState = ArgState.Name;
					}
					continue;
				}
				if(currentState == ArgState.Name) {
					processName = arg;
				}
				if(currentState == ArgState.Inject || currentState == ArgState.Uninject) {
					moduleName = arg;
				}
				if(currentState == ArgState.Inject) {
					task = Task.Inject;
				}
				if(currentState == ArgState.Uninject) {
					task = Task.Uninject;
				}
				currentState = ArgState.None;
			}

			if(task == Task.None) {
				Console.WriteLine("The injector is taskless! Please provide a task!");
				return;
			}

			if(task == Task.Inject) {
				Console.WriteLine("Waiting for \""+processName+"\"...");
				awaitProcess(processName);
				Console.WriteLine("Process found, injecting...");
				InjectDll(moduleName);
				Console.WriteLine("Injection complete");
			}
		}

		public static void awaitProcess(string procName)
        {
            while (true)
            {
                Thread.Sleep(100);
                Process[] possiblilties = Process.GetProcessesByName(procName);
                if (possiblilties.Length < 1)
                {
                    continue;
                }
                Process tempGame = possiblilties[0];
                game = tempGame;
                pHandle = Win32.OpenProcess(0x1F0FFF, true, game.Id);
                break;
            }
        }

		public static void applyAppPackages(string file)
        {
            FileInfo fInfo = new FileInfo(file);
            FileSecurity fSecurity = fInfo.GetAccessControl();
            fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            fInfo.SetAccessControl(fSecurity);
        }

		//Code from https://github.com/erfg12/memory.dll/blob/master/Memory/memory.cs
        public static void InjectDll(string strDllName)
        {
            IntPtr bytesout;

            foreach (ProcessModule pm in game.Modules)
            {
                if (pm.ModuleName.StartsWith("inject", StringComparison.InvariantCultureIgnoreCase))
                    return;
            }

            if (!game.Responding)
                return;

            int lenWrite = strDllName.Length + 1;
            UIntPtr allocMem = Win32.VirtualAllocEx(pHandle, (UIntPtr)null, (uint)lenWrite, 0x00001000 | 0x00002000, 0x04);

            Win32.WriteProcessMemory(pHandle, allocMem, strDllName, (UIntPtr)lenWrite, out bytesout);
            UIntPtr injector = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (injector == null)
                return;

            IntPtr hThread = Win32.CreateRemoteThread(pHandle, (IntPtr)null, 0, injector, allocMem, 0, out bytesout);
            if (hThread == null)
                return;

            int Result = Win32.WaitForSingleObject(hThread, 10 * 1000);
            if (Result == 0x00000080L || Result == 0x00000102L)
            {
                if (hThread != null)
                    Win32.CloseHandle(hThread);
                return;
            }
            Win32.VirtualFreeEx(pHandle, allocMem, (UIntPtr)0, 0x8000);

            if (hThread != null)
                Win32.CloseHandle(hThread);

            return;
        }
	}
}